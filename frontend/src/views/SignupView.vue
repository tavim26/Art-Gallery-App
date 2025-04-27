<template>
  <div class="signup-container">
    <h1>Sign Up</h1>

    <form @submit.prevent="submitSignUp" class="signup-form">
      <label>Name:</label>
      <input type="text" v-model="name" required />

      <label>Email:</label>
      <input type="email" v-model="email" required />

      <label>Password:</label>
      <input type="password" v-model="password" required />

      <label>Phone:</label>
      <input type="text" v-model="phone" required />

      <label>Role:</label>
      <select v-model="role" required>
        <option value="employee">Employee</option>
        <option value="administrator">Administrator</option>
        <option value="manager">Manager</option>
      </select>

      <button type="submit">Register</button>

      <p v-if="error" class="error">{{ error }}</p>
      <p v-if="success" class="success">Registration successful!</p>
    </form>
  </div>
</template>

<script setup>
import useSignUpViewModel from '@/viewmodels/SignupVM';

const { name, email, password, phone, role, success, error, submitSignUp } = useSignUpViewModel();
</script>

<style scoped>
.signup-container {
  max-width: 400px;
  margin: 0 auto;
  padding: 2rem;
  border: 1px solid #ddd;
  border-radius: 8px;
  font-family: sans-serif;
}

.signup-form {
  display: flex;
  flex-direction: column;
  gap: 0.75rem;
}

input, select {
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
