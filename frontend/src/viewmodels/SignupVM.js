import { reactive, toRefs } from 'vue';
import { signUp } from '@/models/AuthModel';

export default function useSignUpViewModel() {
    const state = reactive({
        name: '',
        email: '',
        password: '',
        phone: '',
        role: 'employee',
        success: false,
        error: null
    });

    const submitSignUp = async () => {
        try {
            const res = await signUp({
                name: state.name,
                email: state.email,
                password: state.password,
                phone: state.phone,
                role: state.role
            });
            if (res.status === 200) {
                state.success = true;
                state.error = null;
            }
        } catch (err) {
            state.error = "Registration failed. Please check your data.";
            state.success = false;
        }
    };

    return {
        ...toRefs(state),
        submitSignUp
    };
}
