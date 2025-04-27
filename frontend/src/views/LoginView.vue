<template>
  <div class="login-container">
    <h1>Login</h1>

    <form @submit.prevent="submitLogin" class="login-form">
      <label>Email:</label>
      <input type="email" v-model="email" required />

      <label>Parolă:</label>
      <input type="password" v-model="password" required />

      <button type="submit">Autentificare</button>

      <p v-if="error" class="error">{{ error }}</p>
      <p v-if="success" class="success">Autentificare reușită!</p>
    </form>
  </div>
</template>

<script setup>
import useLoginViewModel from '@/viewmodels/LoginVM';

const { email, password, error, success, submitLogin } = useLoginViewModel();
</script>

<style scoped>
.login-container {
  max-width: 400px;
  margin: 0 auto;
  padding: 2rem;
  border: 1px solid #ddd;
  border-radius: 8px;
  font-family: sans-serif;
}

.login-form {
  display: flex;
  flex-direction: column;
  gap: 0.75rem;
}

input {
  padding: 0.5rem;
  border-radius: 4px;
  border: 1px solid #ccc;
}

button {
  padding: 0.6rem;
  background-color: #2c3e50;
  color: white;
  border: none;
  cursor: pointer;
}

.error {
  color: red;
  font-size: 0.9rem;
}

.success {
  color: green;
  font-size: 0.9rem;
}
</style>
