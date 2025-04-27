import axios from 'axios';

const API_URL = 'http://localhost:8080/api/artworks';

export const fetchArtworks = () => {
    return axios.get(API_URL);
};
