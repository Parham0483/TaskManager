import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";  // ← add this
import api from "../api/axios";
import TaskDetail from "../components/TaskDetail";
import TaskForm from "../components/TaskForm";
import { DndContext, closestCenter } from '@dnd-kit/core';
import { arrayMove, SortableContext, verticalListSortingStrategy } from '@dnd-kit/sortable';
import SortableTask from '../components/SortableTask';

export default function TaskList() {
  const [tasks, setTasks] = useState([]);
  const [selectedTaskId, setSelectedTaskId] = useState(null);
  const [showForm, setShowForm] = useState(false);
  const [users, setUsers] = useState([]);
  const [creationFilter, setCreationFilter] = useState("all");
  const [dueDateFilter, setDueDateFilter] = useState("all");
  const navigate = useNavigate();  // ← add this

  const fetchTasks = async () => {
    try {
      const response = await api.get("/tasks");
      setTasks(response.data);
    } catch (error) {
      console.error("Tasks fetch error:", error.response?.data);
    }
  };

  const fetchUsers = async () => {
    try {
      const response = await api.get("/users");
      setUsers(response.data);
    } catch (error) {
      console.error("Users fetch error:", error.response?.data);
    }
  };

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (!token) {
      navigate('/');
      return;
    }
    fetchTasks();
    fetchUsers();
  }, [navigate]);

  const updateStatus = async (taskId, newStatus) => {
    const token = localStorage.getItem("token");
    const task = tasks.find((t) => t.id === taskId);
    if (!task || task.status === newStatus) return;

    try {
      await api.put(`/tasks/${taskId}`, { ...task, status: newStatus }, {
        headers: { Authorization: `Bearer ${token}` },
      });
      fetchTasks(token);
    } catch (err) {
      alert("Failed to update status");
    }
  };

  const handleDragEnd = async (event) => {
    const { active, over } = event;
    if (!over || active.id === over.id) return;

    const activeTask = tasks.find((t) => t.id.toString() === active.id);
    const overTask = tasks.find((t) => t.id.toString() === over.id);

    if (activeTask && overTask && activeTask.status === overTask.status) {
      const sameStatusTasks = tasks.filter(t => t.status === activeTask.status);
      const oldIndex = sameStatusTasks.findIndex(t => t.id === activeTask.id);
      const newIndex = sameStatusTasks.findIndex(t => t.id === overTask.id);
      const updatedOrder = arrayMove(sameStatusTasks, oldIndex, newIndex);
      const updatedTasks = tasks.map(t =>
        t.status !== activeTask.status ? t : updatedOrder.find(u => u.id === t.id)
      );
      setTasks(updatedTasks);
    } else if (activeTask && overTask && activeTask.status !== overTask.status) {
      await updateStatus(activeTask.id, overTask.status);
    }
  };

  // ---- Creation Filter
  const applyCreationFilter = (taskList) => {
    const now = new Date();
    const today = new Date();
    const startOfWeek = new Date(today);
    startOfWeek.setDate(today.getDate() - today.getDay());
    const startOfLastWeek = new Date(startOfWeek);
    startOfLastWeek.setDate(startOfLastWeek.getDate() - 7);
    const startOfMonth = new Date(today.getFullYear(), today.getMonth(), 1);

    return taskList.filter(task => {
      const created = new Date(task.createdAt);
      switch (creationFilter) {
        case "today": return created.toDateString() === today.toDateString();
        case "thisWeek": return created >= startOfWeek;
        case "lastWeek": return created >= startOfLastWeek && created < startOfWeek;
        case "thisMonth": return created >= startOfMonth;
        default: return true;
      }
    });
  };

  // ---- Due Date Filter
  const applyDueDateFilter = (taskList) => {
    const now = new Date();
    const today = new Date();
    const endOfWeek = new Date(today);
    endOfWeek.setDate(today.getDate() + (7 - today.getDay()));
    const endOfMonth = new Date(today.getFullYear(), today.getMonth() + 1, 0);

    return taskList.filter(task => {
      if (!task.dueDate) return dueDateFilter === "all";
      const due = new Date(task.dueDate);
      switch (dueDateFilter) {
        case "overdue": return due < now;
        case "dueToday": return due.toDateString() === today.toDateString();
        case "dueThisWeek": return due >= now && due <= endOfWeek;
        case "dueThisMonth": return due >= now && due <= endOfMonth;
        default: return true;
      }
    });
  };

  const filteredTasks = applyDueDateFilter(applyCreationFilter(tasks));

  const grouped = { Todo: [], Doing: [], Done: [] };
  filteredTasks.forEach(task => { if (grouped[task.status]) grouped[task.status].push(task); });

  return (
    <div>
      <h2>Tasks</h2>
      <button onClick={() => setShowForm(true)}>+ New Task</button>
      <div style={{ marginTop: "10px", marginBottom: "10px" }}>
        <label>
          Created:
          <select value={creationFilter} onChange={(e) => setCreationFilter(e.target.value)} style={{ marginLeft: 5, marginRight: 15 }}>
            <option value="all">All</option>
            <option value="today">Today</option>
            <option value="thisWeek">This Week</option>
            <option value="lastWeek">Last Week</option>
            <option value="thisMonth">This Month</option>
          </select>
        </label>
        <label>
          Due:
          <select value={dueDateFilter} onChange={(e) => setDueDateFilter(e.target.value)} style={{ marginLeft: 5 }}>
            <option value="all">All</option>
            <option value="overdue">Overdue</option>
            <option value="dueToday">Due Today</option>
            <option value="dueThisWeek">Due This Week</option>
            <option value="dueThisMonth">Due This Month</option>
          </select>
        </label>
      </div>

      {showForm && <TaskForm onComplete={() => { setShowForm(false); fetchTasks(localStorage.getItem("token")); }} />}
      {selectedTaskId && <TaskDetail id={selectedTaskId} onComplete={() => { setSelectedTaskId(null); fetchTasks(localStorage.getItem("token")); }} />}

      <DndContext collisionDetection={closestCenter} onDragEnd={handleDragEnd}>
        <div style={{ display: "flex", gap: "20px", marginTop: "20px" }}>
          {Object.keys(grouped).map(status => (
            <div key={status} style={{ flex: 1 }}>
              <h3>{status}</h3>
              <SortableContext items={grouped[status].map(task => task.id.toString())} strategy={verticalListSortingStrategy}>
                {grouped[status].length === 0 && <p>No tasks</p>}
                {grouped[status].map(task => (
                  <SortableTask key={task.id} id={task.id.toString()} task={task} onDetailClick={() => setSelectedTaskId(task.id)} />
                ))}
              </SortableContext>
            </div>
          ))}
        </div>
      </DndContext>
    </div>
  );
}
