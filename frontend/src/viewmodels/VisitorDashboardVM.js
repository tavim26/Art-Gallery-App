import { reactive, toRefs } from 'vue';
import { fetchArtists } from '@/models/ArtistModel';
import { fetchArtworks } from '@/models/ArtworkModel';

export default function useVisitorDashboardViewModel() {
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

    return {
        ...toRefs(state),
        loadData,
        filteredArtists,
        filteredArtworks
    };
}
