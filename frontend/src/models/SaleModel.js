import axios from 'axios';

const API_URL = 'http://localhost:8080/api/sales';

export const sellArtwork = (artworkId, employeeId, saleDate, price) => {
    return axios.post(API_URL, {
        artworkId,
        employeeId,
        saleDate,
        price
    });
};
