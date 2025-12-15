<script lang="ts">
    import LayoutLoader from "$lib/components/layouts/LayoutLoader.svelte";
    import NavMenu from "./NavMenu.svelte";
    import NavLogo from "./NavLogo.svelte";

	let { children } = $props();
</script>

<LayoutLoader>
    <main>
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

<style lang="scss">
    @use "../../lib/components/styling/vars.scss" as c;

    main {
        height: 100%;
        display: flex;
        flex-direction: column;
        gap: 1rem;
        padding: 1rem;

        .navbar {
            flex: none;
            box-sizing: border-box;
            height: 60px;
            border-radius: 20px;
            border: c.$border 1px solid;
            background: c.$sidebar;
            display: flex;
            flex-direction: row;
            flex-wrap: nowrap;
            align-items: center;
            justify-content: space-between;
            padding: 1rem;

            .navbar-actions {
                flex: none;
                display: flex;
                flex-direction: row;
                align-items: center;
                flex-wrap: nowrap;
            }
        }

        #content {
            flex: 1;
        }
    }
</style>
