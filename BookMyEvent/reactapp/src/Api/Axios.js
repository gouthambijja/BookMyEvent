import axios from 'axios';
const BASE_URL = 'https://localhost:7101';

export default axios.create({
    baseURL: BASE_URL
});

export const axiosPrivate = axios.create({
    baseURL: BASE_URL,
    withCredentials: true
});