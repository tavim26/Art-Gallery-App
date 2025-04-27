import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '@/views/HomeView.vue';
import LoginView from '@/views/auth/LoginView.vue';
import SignUpView from '@/views/auth/SignUpView.vue';
// (GalleryView vine mai târziu)

const routes = [
    { path: '/', name: 'Home', component: HomeView },
    { path: '/login', name: 'Login', component: LoginView },
    { path: '/signup', name: 'SignUp', component: SignUpView },
    { path: '/gallery', name: 'Gallery', component: HomeView } // deocamdată redirecționăm la Home, va fi înlocuit
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;
