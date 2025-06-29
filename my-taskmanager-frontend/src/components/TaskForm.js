import { useState, useEffect } from 'react';
import api from '../api/axios';

function TaskForm({ onComplete }) {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [status, setStatus] = useState('Todo');
  const [assigneeId, setAssigneeId] = useState('');
  const [currentUser, setCurrentUser] = useState(null);

  useEffect(() => {
    // Get current user and set as the only assignee option
    const user = JSON.parse(localStorage.getItem('user'));
    if (user?.userId) {
      setCurrentUser(user);
      setAssigneeId(user.userId.toString());
    } else {
      console.error('No current user found');
    }
  }, []);

  const handleCreateTask = async (e) => {
    e.preventDefault();

    // Ensure we have a current user
    if (!currentUser?.userId) {
      alert('Unable to create task: User not found');
      return;
    }

    try {
      const newTask = {
        title,
        description,
        status,
        assigneeId: parseInt(currentUser.userId), // Always use current user's ID
      };

      await api.post('/tasks', newTask);
      alert('Task created successfully!');

      // Call onComplete to close form and reload tasks
      if (onComplete) onComplete();
    } catch (error) {
      console.error('Error creating task:', error);
      alert('Task creation failed. Please try again.');
    }
  };

  return (
    <form onSubmit={handleCreateTask}>
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
        <input
          type="text"
          value={currentUser?.userName || 'Loading...'}
          readOnly
          style={{
            backgroundColor: '#f5f5f5',
            cursor: 'not-allowed',
            border: '1px solid #ddd',
            padding: '8px'
          }}
        />
        <small style={{ color: '#666', display: 'block', marginTop: '4px' }}>
          Tasks can only be assigned to yourself
        </small>
      </div>

      <button type="submit">Create Task</button>
    </form>
  );
}

export default TaskForm;