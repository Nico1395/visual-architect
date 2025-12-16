<script lang="ts">
    import { waitLocale } from 'svelte-i18n';
    import { onMount } from 'svelte';
    import { setupI18n } from '$lib/i18n';
    import { ModeWatcher } from "mode-watcher";
    import '$lib/i18n'; 

    let initialized = $state(false);

    onMount(async () => {
        await setupI18n();
        await waitLocale(); 
        
        initialized = true;
    });

    let { children } = $props();
</script>


{#if initialized}
    {@render children()}
{:else}
    <h1>
        Loading
    </h1>
{/if}
