import api from '../api/axios';
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';         

function UserList() {
    const [users, setUsers] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        fetchUsers();
    }, []);

    const fetchUsers = async () => {
        try {
            const response = await api.get('/users');
            setUsers(response.data);
        } catch (error) {
            console.error('Error fetching users:', error);
            alert('Failed to fetch users. Please try again later.');
        }
    };

    const handleViewUser = (userId) => {
        navigate(`/users/${userId}`);
    };

    return (
        <div className="user-list-container">
            <h2>Users</h2>
            <div className="users-cards">
                {users.map((user) => (
                    <div key={user.id} className="user-card" onClick={() => handleViewUser(user.id)}>
                        <h3>{user.name}</h3>
                        <p>Email: {user.email}</p>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default UserList;