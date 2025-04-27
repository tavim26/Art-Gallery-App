import { reactive, toRefs } from 'vue';
import { fetchArtists } from '@/models/ArtistModel';
import { fetchArtworks } from '@/models/ArtworkModel';
import { sellArtwork } from '@/models/SaleModel';
import { saveAs } from 'file-saver';

export default function useEmployeeDashboardViewModel() {
    const state = reactive({
        artists: [],
        artworks: [],
        artistSearch: '',
        artworkSearch: '',
        selectedArtist: '',
        selectedType: '',
        maxPrice: 10000,
    });

    const loadData = async () => {
        const artistRes = await fetchArtists();
        state.artists = artistRes.data;

        const artworkRes = await fetchArtworks();
        state.artworks = artworkRes.data;
    };

    const filteredArtists = () => {
        return state.artists.filter(a =>
            a.name.toLowerCase().includes(state.artistSearch.toLowerCase())
        );
    };

    const filteredArtworks = () => {
        return state.artworks.filter(a => {
            const matchesTitle = a.title.toLowerCase().includes(state.artworkSearch.toLowerCase());
            const matchesArtist = !state.selectedArtist || a.artistName === state.selectedArtist;
            const matchesType = !state.selectedType || a.type === state.selectedType;
            const matchesPrice = a.price <= state.maxPrice;
            return matchesTitle && matchesArtist && matchesType && matchesPrice;
        });
    };

    const exportArtworksToCSV = () => {
        const headers = ["Title", "Year Created", "Type", "Artist Name", "Price"];
        const rows = filteredArtworks().map(a => [
            a.title, a.yearCreated, a.type, a.artistName, a.price
        ]);

        let csvContent = headers.join(",") + "\n" +
            rows.map(e => e.join(",")).join("\n");

        const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });
        saveAs(blob, "artworks.csv");
    };

    const exportArtworksToJSON = () => {
        const blob = new Blob([JSON.stringify(filteredArtworks(), null, 2)], { type: 'application/json;charset=utf-8;' });
        saveAs(blob, "artworks.json");
    };

    const sellArtworkById = async (artwork, employeeId) => {
        try {
            const saleDate = new Date().toISOString();
            await sellArtwork(artwork.id, employeeId, saleDate, artwork.price);
            alert(`Artwork "${artwork.title}" sold successfully!`);
        } catch (error) {
            console.error(error);
            alert("Failed to sell artwork.");
        }
    };

    return {
        ...toRefs(state),
        loadData,
        filteredArtists,
        filteredArtworks,
        exportArtworksToCSV,
        exportArtworksToJSON,
        sellArtworkById
    };
}
