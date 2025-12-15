<script lang="ts">
    import "./NavLogo.scss";
	import favicon from '$lib/assets/visual-architect-logo_48x48.png';
    import { _ } from 'svelte-i18n';
</script>

<a class="navbar-logo" href="/app/home">
    <img src="{favicon}" alt="{favicon}" height="32" width="32" />

    <h2>{$_('common.app.name')}</h2>
</a>
