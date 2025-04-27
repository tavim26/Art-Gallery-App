import { reactive, toRefs } from 'vue';
import { login } from '@/models/AuthModel';

export default function useLoginViewModel() {
    const state = reactive({
        email: '',
        password: '',
        error: null,
        success: false
    });

    const submitLogin = async () => {
        try {
            const res = await login(state.email, state.password);
            if (res.status === 200) {
                state.success = true;
                state.error = null;
            }
        } catch (err) {
            state.error = "Autentificare eșuată. Verifică datele.";
            state.success = false;
        }
    };

    return {
        ...toRefs(state),
        submitLogin
    };
}
