<script lang="ts">
    import "./+layout.scss";
    import LayoutLoader from "$lib/components/layouts/LayoutLoader.svelte";
    import NavMenu from "./NavMenu.svelte";
    import NavLogo from "./NavLogo.svelte";

	let { children } = $props();
</script>

<LayoutLoader>
    <main class="app-layout">
        <nav class="navbar">
            <NavLogo />

            <div class="navbar-action">
                <NavMenu />
            </div>
        </nav>

        <div id="content">
            {@render children()}
        </div>
    </main>
</LayoutLoader>
