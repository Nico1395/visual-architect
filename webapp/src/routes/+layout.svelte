<script lang="ts">
	import './layout.css';
  	import { ModeWatcher } from "mode-watcher";
	import favicon from '$lib/assets/favicon.png';

	let { children } = $props();
</script>

<svelte:head><link rel="icon" href={favicon} /></svelte:head>

<ModeWatcher />
{@render children()}
