<script lang="ts">
	import favicon from '$lib/assets/visual-architect-logo_48x48.png';
    import { _ } from 'svelte-i18n';
</script>

<a class="navbar-logo" href="/app/home">
    <img src="{favicon}" alt="{favicon}" height="32" width="32" />

    <h2>{$_('common.app.name')}</h2>
</a>

<style lang="scss">
    .navbar-logo {
        flex: none;
        display: flex;
        flex-direction: row;
        flex-wrap: nowrap;
        align-items: center;
        gap: 0.5rem;

        img {
            height: 32px;
            width: 32px;
        }

        > h2 {
            margin: 0;
            line-height: normal;
        }
    }
</style>
