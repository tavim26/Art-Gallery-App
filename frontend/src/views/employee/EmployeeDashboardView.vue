<template>
  <div class="dashboard-container">

    <!-- Artists Section -->
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
          <th>Actions</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="artist in filteredArtists()" :key="artist.id">
          <td>{{ artist.name }}</td>
          <td>{{ artist.birthDate }}</td>
          <td>{{ artist.birthplace }}</td>
          <td>{{ artist.nationality }}</td>
          <td>
            <button @click="deleteArtist(artist.id)">Delete</button>
          </td>
        </tr>
        </tbody>
      </table>

      <div class="artist-images">
        <img v-for="artist in filteredArtists()" :src="artist.photo" :alt="artist.name" class="artist-image" />
      </div>

      <!-- Add Artist Form -->
      <h3>Add Artist</h3>
      <form @submit.prevent="addArtist" class="form-section">
        <input v-model="newArtist.name" type="text" placeholder="Name" required />
        <input v-model="newArtist.birthDate" type="date" placeholder="Birth Date" required />
        <input v-model="newArtist.birthplace" type="text" placeholder="Birthplace" required />
        <input v-model="newArtist.nationality" type="text" placeholder="Nationality" required />
        <input v-model="newArtist.photo" type="text" placeholder="Photo URL" required />
        <button type="submit">Add Artist</button>
      </form>
    </section>

    <!-- Artworks Section -->
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

      <div class="export-buttons">
        <button @click="exportArtworksToCSV">Export to CSV</button>
        <button @click="exportArtworksToJSON">Export to JSON</button>
      </div>

      <table>
        <thead>
        <tr>
          <th>Title</th>
          <th>Year Created</th>
          <th>Type</th>
          <th>Artist</th>
          <th>Price (€)</th>
          <th>Actions</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="artwork in filteredArtworks()" :key="artwork.id">
          <td>{{ artwork.title }}</td>
          <td>{{ artwork.yearCreated }}</td>
          <td>{{ artwork.type }}</td>
          <td>{{ artwork.artistName }}</td>
          <td>{{ artwork.price }}</td>
          <td>
            <button @click="sellArtworkById(artwork, 1)">Sell</button>
            <button @click="deleteArtwork(artwork.id)">Delete</button>
          </td>
        </tr>
        </tbody>
      </table>

      <div class="artwork-images">
        <img v-for="artwork in filteredArtworks()" :src="artwork.imageUrls[0]" :alt="artwork.title" class="artwork-image" />
      </div>

      <!-- Add Artwork Form -->
      <h3>Add Artwork</h3>
      <form @submit.prevent="addArtwork" class="form-section">
        <input v-model="newArtwork.title" type="text" placeholder="Title" required />
        <input v-model="newArtwork.yearCreated" type="number" placeholder="Year Created" required />
        <input v-model="newArtwork.type" type="text" placeholder="Type" required />
        <input v-model="newArtwork.artistName" type="text" placeholder="Artist Name" required />
        <input v-model="newArtwork.price" type="number" placeholder="Price (€)" required />
        <input v-model="newArtwork.imageUrls[0]" type="text" placeholder="Image URL" required />
        <button type="submit">Add Artwork</button>
      </form>
    </section>

  </div>
</template>

<script setup>
import useEmployeeDashboardViewModel from '@/viewmodels/EmployeeDashboardVM';

const {
  artists,
  artworks,
  artistSearch,
  artworkSearch,
  selectedArtist,
  selectedType,
  maxPrice,
  newArtist,
  newArtwork,
  loadData,
  filteredArtists,
  filteredArtworks,
  exportArtworksToCSV,
  exportArtworksToJSON,
  sellArtworkById,
  addArtist,
  addArtwork,
  deleteArtist,
  deleteArtwork
} = useEmployeeDashboardViewModel();

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
  margin: 0.5rem 0;
  padding: 0.5rem;
  font-size: 1rem;
}

button {
  margin: 0.3rem;
  padding: 0.5rem 1rem;
  border: none;
  background-color: #2c3e50;
  color: white;
  border-radius: 5px;
  cursor: pointer;
}

button:hover {
  background-color: #34495e;
}

.export-buttons {
  margin-top: 1rem;
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

.form-section {
  margin-top: 2rem;
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}
</style>
