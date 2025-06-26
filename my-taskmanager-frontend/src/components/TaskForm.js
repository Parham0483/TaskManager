import { useState, useEffect } from 'react';
import api from '../api/axios';

function TaskForm({ onComplete }) {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [status, setStatus] = useState('Todo');
  const [assigneeId, setAssigneeId] = useState('');
  const [users, setUsers] = useState([]);

  useEffect(() => {
    fetchUsers();
    const currentUser = JSON.parse(localStorage.getItem('user'));
    if (currentUser?.id) {
      setAssigneeId(currentUser.id);
    }
  }, []);

  const fetchUsers = async () => {
    try {
      const response = await api.get('/users');
      setUsers(response.data);
    } catch (error) {
      console.error('Error fetching users:', error);
      alert('Failed to load users. Please try again.');
    }
  };

  const handleCreateTask = async (e) => {
    e.preventDefault();

    try {
      const newTask = {
        title,
        description,
        status,
        assigneeId: parseInt(assigneeId),
      };

      await api.post('/tasks', newTask);
      alert('Task created successfully!');
      
      // ✅ Call onComplete to close form and reload tasks
      if (onComplete) onComplete();
    } catch (error) {
      console.error('Error creating task:', error);
      alert('Task creation failed. Please try again.');
    }
  };

  return (
    <form onSubmit={handleCreateTask} className="task-form">
      <h2>Create New Task</h2>

      <div className="form-group">
        <label>Title:</label>
        <input
          type="text"
          value={title}
          onChange={e => setTitle(e.target.value)}
          required
        />
      </div>

      <div className="form-group">
        <label>Description:</label>
        <textarea
          value={description}
          onChange={e => setDescription(e.target.value)}
          required
        />
      </div>

      <div className="form-group">
        <label>Status:</label>
        <select
          value={status}
          onChange={e => setStatus(e.target.value)}
        >
          <option value="Todo">Todo</option>
          <option value="Doing">Doing</option>
          <option value="Done">Done</option>
        </select>
      </div>

      <div className="form-group">
        <label>Assignee:</label>
        <select
          value={assigneeId}
          onChange={e => setAssigneeId(e.target.value)}
          required
        >
          <option value="">Select a user</option>
          {users.length > 0 ? (
            users.map(user => (
              <option key={user.id} value={user.id}>
                {user.name}
              </option>
            ))
          ) : (
            <option disabled>Loading users...</option>
          )}
        </select>
      </div>

      <button type="submit">Create Task</button>
    </form>
  );
}

export default TaskForm;
