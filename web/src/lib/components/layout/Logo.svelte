<script lang="ts">
    import '../../styles/controls.scss';
	import logo_48x48 from '$lib/assets/visual-architect-logo_48x48.png';
</script>

<a class="stealthy-a logo" href="/home">
    <img src="{logo_48x48}" alt="{logo_48x48}" height="36" width="36" />

    <h3>
        Visual Architect
    </h3>
</a>

<style lang="scss">
    .logo {
        width: fit-content;
        flex: none;
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 0.5rem;

        > * {
            margin: 0;
        }
    }
</style>
