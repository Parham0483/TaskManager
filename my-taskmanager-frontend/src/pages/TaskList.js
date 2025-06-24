import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../api/axios";

function TasksList() {
    const [tasks, setTasks] = useState([]);
    const [statusFilter, setStatusFilter] = useState('');
    const navigate = useNavigate();

    useEffect(() => {
        fetchTasks();
    }, [statusFilter]);

    const fetchTasks = async () => {
        try {
            const endpoint = statusFilter
                ? `/tasks/status/${statusFilter}`
                : '/tasks';
            const response = await api.get(endpoint);
            setTasks(response.data);
        } catch (error) {
            console.error('Error fetching tasks:', error);
            alert('Failed to fetch tasks. Please try again later.');
        }
    };

    const handleViewTask = (taskId) => {
        navigate(`/tasks/${taskId}`);
    };

    const handleCreateTask = () => {
        navigate('/tasks/create');
    };

    return (
        <div className="tasks-list-container">
            <h2>My Tasks</h2>

            <div className="filter-container">
                <select
                    value={statusFilter}
                    onChange={(e) => setStatusFilter(e.target.value)}
                >
                    <option value="">All Tasks</option>
                    <option value="Todo">Todo</option>
                    <option value="Doing">Doing</option>
                    <option value="Done">Done</option>
                </select>

                <button onClick={handleCreateTask}>+ New Task</button>
            </div>

            <div className="tasks-cards">
                {tasks.map((task) => (
                    <div key={task.id} className="task-card" onClick={() => handleViewTask(task.id)}>
                        <h3>{task.title || "(No title)"}</h3>
                        <p>{task.description || "No description provided."}</p>
                        <p>Status: {task.status}</p>
                        <p>Assigned to: {task.assigneeName || "Unassigned"}</p>
                        <p>Created: {task.createdAt ? new Date(task.createdAt).toLocaleDateString() : "N/A"}</p>
                        <p>Due: {task.handedIn ? new Date(task.handedIn).toLocaleDateString() : "N/A"}</p>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default TasksList;
