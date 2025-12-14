<script lang="ts">
    import { goto } from '$app/navigation';
    import { onMount } from 'svelte';

    onMount(() => {
        const isAuthenticated = true;

        if (isAuthenticated) {
            goto('/home', { replaceState: true });
        } else {
            goto('/auth/login', { replaceState: true });
        }
    });
</script>
