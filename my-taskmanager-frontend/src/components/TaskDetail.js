import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../api/axios';

function TaskDetail({id, onComplete}) {
  const navigate = useNavigate();

  const [task, setTask] = useState(null);
  const [status, setStatus] = useState('');
  const [assigneeId, setAssigneeId] = useState('');
  const [users, setUsers] = useState([]);

  useEffect(() => {
    fetchTask();
    fetchUsers();
  }, []);

  const fetchTask = async () => {
    try {
      const res = await api.get(`/tasks/${id}`);
      const data = res.data;
      setTask(data);
      setStatus(data.status);
      setAssigneeId(data.assigneeId);
    } catch (err) {
      console.error('Error fetching task:', err);
      alert('Failed to load task.');
    }
  };

  const fetchUsers = async () => {
    try {
      const res = await api.get('/users');
      setUsers(res.data);
    } catch (err) {
      console.error('Error fetching users:', err);
      alert('Failed to load users.');
    }
  };

  const handleUpdate = async () => {
    if (!task) return;

    try {
      await api.put(`/tasks/${id}`, {
        ...task,
        status,
        assigneeId: parseInt(assigneeId),
      });
      alert('Task updated successfully.');
      if (onComplete) onComplete(); 
    } catch (err) {
      console.error('Error updating task:', err);
      alert('Failed to update task.');
    }
  };

  const handleDelete = async () => {
    try {
      await api.delete(`/tasks/${id}`);
      alert('Task deleted.');
      if (onComplete) onComplete();
    } catch (err) {
      console.log('Attempting to delete task with ID:', id);
      console.error('Error deleting task:', err.response?.data || err.message);
      alert('Failed to delete task.');
    }
  };

  if (!task) return <div>Loading task details...</div>;

  return (
    <div className="task-detail-container">
      <h2>Task #{task.id}</h2>

      {onComplete && (
      <button onClick={onComplete} className="close-button">
       Close
      </button>
       )}
      <div className="task-field">
        <label>Title:</label>
        <p>{task.title || 'No title provided.'}</p>
      </div>

      <div className="task-field">
        <label>Description:</label>
        <p>{task.description || 'No description provided.'}</p>
      </div>

      <div className="task-field">
        <label>Status:</label>
        <select
          value={status}
          onChange={(e) => setStatus(e.target.value)}
        >
          <option value="Todo">Todo</option>
          <option value="Doing">Doing</option>
          <option value="Done">Done</option>
        </select>
      </div>

      <div className="task-field">
        <label>Assignee:</label>
        <select
          value={assigneeId}
          onChange={(e) => setAssigneeId(e.target.value)}
        >
          {users.map((u) => (
            <option key={u.id} value={u.id}>
              {u.name}
            </option>
          ))}
        </select>
      </div>

      {task.handedIn && (
        <div className="task-field">
          <label>Handed In:</label>
          <span>{new Date(task.handedIn).toLocaleString()}</span>
        </div>
      )}

      <div className="task-field">
        <label>Created At:</label>
        <span>{new Date(task.createdAt).toLocaleString()}</span>
      </div>

      <div className="task-buttons">
        <button onClick={handleUpdate}>Save Changes</button>
        <button onClick={handleDelete} className="danger">Delete Task</button>
      </div>
    </div>
  );
}

export default TaskDetail;
