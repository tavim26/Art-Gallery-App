import axios from 'axios';

const API_URL = 'http://localhost:8080/api/artists';

export const fetchArtists = () => {
    return axios.get(API_URL);
};
