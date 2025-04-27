import axios from 'axios';

const API_URL = 'http://localhost:8080/api/auth';

export const login = (email, password) => {
    return axios.post(`${API_URL}/login`, null, {
        params: { email, password }
    });
};

export const signUp = (userData) => {
    return axios.post(`${API_URL}/signup`, userData);
};
