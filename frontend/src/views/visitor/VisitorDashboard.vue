<template>
  <div class="dashboard-container">

    <section class="artists-section">
      <h2>Artists</h2>
      <input type="text" v-model="artistSearch" placeholder="Search artists by name" />
      <table>
        <thead>
        <tr>
          <th>Name</th>
          <th>Birth Year</th>
          <th>Birthplace</th>
          <th>Nationality</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="artist in filteredArtists()" :key="artist.id">
          <td>{{ artist.name }}</td>
          <td>{{ artist.birthDate }}</td>
          <td>{{ artist.birthplace }}</td>
          <td>{{ artist.nationality }}</td>
        </tr>
        </tbody>
      </table>
      <div class="artist-images">
        <img v-for="artist in filteredArtists()" :src="artist.photo" :alt="artist.name" class="artist-image" />
      </div>
    </section>

    <section class="artworks-section">
      <h2>Artworks</h2>
      <input type="text" v-model="artworkSearch" placeholder="Search artworks by title" />

      <div class="filters">
        <select v-model="selectedArtist">
          <option value="">All Artists</option>
          <option v-for="artist in artists" :key="artist.id" :value="artist.name">
            {{ artist.name }}
          </option>
        </select>

        <select v-model="selectedType">
          <option value="">All Types</option>
          <option value="painting">Painting</option>
          <option value="sculpture">Sculpture</option>
          <option value="drawing">Drawing</option>
        </select>

        <div>
          <label>Max Price: €{{ maxPrice }}</label>
          <input type="range" min="0" max="10000" step="50" v-model="maxPrice" />
        </div>
      </div>

      <table>
        <thead>
        <tr>
          <th>Title</th>
          <th>Year Created</th>
          <th>Type</th>
          <th>Artist</th>
          <th>Price (€)</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="artwork in filteredArtworks()" :key="artwork.id">
          <td>{{ artwork.title }}</td>
          <td>{{ artwork.yearCreated }}</td>
          <td>{{ artwork.type }}</td>
          <td>{{ artwork.artistName }}</td>
          <td>{{ artwork.price }}</td>
        </tr>
        </tbody>
      </table>

      <div class="artwork-images">
        <img v-for="artwork in filteredArtworks()" :src="artwork.imageUrls[0]" :alt="artwork.title" class="artwork-image" />
      </div>

    </section>

  </div>
</template>

<script setup>
import useVisitorDashboardViewModel from '@/viewmodels/VisitorDashboardVM.js';

const {
  artists,
  artworks,
  artistSearch,
  artworkSearch,
  selectedArtist,
  selectedType,
  maxPrice,
  loadData,
  filteredArtists,
  filteredArtworks
} = useVisitorDashboardViewModel();

loadData();
</script>

<style scoped>
.dashboard-container {
  padding: 2rem;
}

section {
  margin-bottom: 4rem;
}

table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 1rem;
}

th, td {
  border: 1px solid #ccc;
  padding: 0.75rem;
  text-align: left;
}

input, select {
  margin: 1rem 0;
  padding: 0.5rem;
  font-size: 1rem;
}

.artist-images, .artwork-images {
  display: flex;
  flex-wrap: wrap;
  gap: 1rem;
  margin-top: 1rem;
}

.artist-image, .artwork-image {
  width: 150px;
  height: auto;
  border-radius: 8px;
}
</style>
