<script lang="ts">
	import favicon from '$lib/assets/favicon.svg';

	let { children } = $props();
</script>

<svelte:head>
	<link rel="icon" href={favicon} />
</svelte:head>

<main>
	<header>

	</header>

	<div>
		{@render children()}
	</div>

	<footer>

	</footer>
</main>

<style lang="scss">
	$test: #234223;
</style>
