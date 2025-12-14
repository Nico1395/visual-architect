<script lang="ts">
	let { children } = $props();
</script>

<main class="padded-content-wrapper">
	<div id="padded-content">
		{@render children()}
	</div>
</main>

<style lang="scss">
    .padded-content-wrapper {
        margin: 0 auto;
        width: 1200px;
        padding: 0 1rem;
        box-sizing: border-box;

        > .padded-content {
            width: 100%;
        }
    }

    @media (max-width: calc(1200px - 1rem)) {
        .padded-content-wrapper {
            width: 100%;
        }
    }
</style>
