<script>
    import "./+layout.scss";
    import LayoutLoader from "$lib/components/layouts/LayoutLoader.svelte";

    let { children } = $props();
</script>

<LayoutLoader>
    <main class="auth-layout">
        {@render children()}
    </main>
</LayoutLoader>
