import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import api from "../api/axios";

function LoginPage() {
    const [phoneNo, setPhoneNo] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();
        try {
            const response = await api.post('users/login', { phoneNo, password });
            const { token, ...userData } = response.data;

            if (token) {
                localStorage.setItem('token', token);
                localStorage.setItem('user', JSON.stringify(userData));

            } else {
                throw new Error('No token received from server.');
            }

            if (userData.role === 'admin') {
                navigate('/users');
            } else {
                navigate('/tasks');
            }
        } catch (error) {
            alert('Login failed. Please check your credentials.');
        }
    };

    return (
        <div className="login-container">
            <h2>Login</h2>
            <form onSubmit={handleLogin}>
                <div>
                    <label htmlFor="phoneNo">Phone No:</label>
                    <input
                        type="text"
                        id="phoneNo"
                        value={phoneNo}
                        onChange={(e) => setPhoneNo(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="password">Password:</label>
                    <input
                        type="password"
                        id="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                <button type="submit">Login</button>
                <p style={{ marginTop: '10px' }}>
                    Don't have an account? <Link to="/register">Register here</Link>
                </p>
            </form>
        </div>
    );
}

export default LoginPage;
