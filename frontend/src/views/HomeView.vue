<template>
  <div class="home-container">
    <h1>Welcome to Art Gallery</h1>

    <div class="buttons">
      <button @click="goToLogin">Go to Login</button>
      <button @click="goToSignUp">Go to Sign Up</button>
      <button @click="continueWithoutAccount">Continue without account</button>
    </div>
  </div>
</template>

<script setup>
import { useRouter } from 'vue-router';

const router = useRouter();

const goToLogin = () => {
  router.push('/login');
};

const goToSignUp = () => {
  router.push('/signup');
};

const continueWithoutAccount = () => {
  router.push('/gallery'); // momentan, placeholder
};
</script>

<style scoped>
.home-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 5rem;
}

.buttons {
  display: flex;
  flex-direction: column;
  gap: 1rem;
  margin-top: 2rem;
}

button {
  padding: 0.8rem 1.5rem;
  border: none;
  background-color: #2c3e50;
  color: white;
  font-size: 1rem;
  border-radius: 8px;
  cursor: pointer;
  width: 250px;
}

button:hover {
  background-color: #34495e;
}
</style>
