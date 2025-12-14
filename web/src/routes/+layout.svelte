<script lang="ts">
    import '../lib/styles/containers.scss';
    import '../lib/styles/layout.scss';
    import favicon from '$lib/assets/favicon.png';
    import Logo from '../lib/components/layout/Logo.svelte';
    import Button from '$lib/components/controls/Button.svelte';
    import DropdownButton from '$lib/components/controls/DropdownButton.svelte';

    let { children } = $props();
</script>

<svelte:head>
    <link rel="icon" href={favicon} />
</svelte:head>

<main>
    <header class="navbar">
        <nav class="content-container large navbar-container">
            <Logo />

            <div class="navbar-actions">
                <DropdownButton>
                    {#snippet buttonChildren()}
                        User
                    {/snippet}

                    {#snippet dropdownChildren()}
                        <Button iconLeft="ai-gear">
                            Settings
                        </Button>

                        <Button iconLeft="ai-door">
                            Log out
                        </Button>
                    {/snippet}
                </DropdownButton>
            </div>
        </nav>
    </header>

    <div id="content">
        {@render children()}
    </div>

    <footer class="footer">
        <div>
            footer
        </div>
    </footer>
</main>

<style lang="scss">
    $_padding: 1rem;
    
    main {
        height: 100%;
        display: flex;
        flex-direction: column;
    }

    .navbar {
        flex: none;
        padding: $_padding;

        > .navbar-container {
            height: 60px;
            display: flex;
            flex-direction: row;
            align-items: center;
            justify-content: space-between;
            
            > .navbar-actions {
                flex: none;
                display: flex;
                flex-direction: row;
                gap: 0.5rem;
                align-items: center;
                justify-self: end;
            }
        }
    }

    #content {
        flex: 1;
        padding: 0 $_padding;
    }

    .footer {
        height: 60px;
        flex: none;
        padding: $_padding;
    }
</style>
