import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5207/api', // Hardcode for testing
});

api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');

    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    } else {
      console.log('❌ No token found in localStorage');
    }

    return config;
  },
  (error) => {
    console.error('❌ Request interceptor error:', error);
    return Promise.reject(error);
  }
);
// Response interceptor with debugging
api.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    console.error('❌ Response interceptor error:', error);
    return Promise.reject(error);
  }
);

export default api;