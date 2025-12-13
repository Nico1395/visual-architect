<script lang="ts">
  import Router from 'svelte-spa-router';
  import Counter from './lib/Counter.svelte'
  import NotFound from './lib/pages/NotFound.svelte';

  const routes = {
    '/': Counter,
    '*': NotFound,
  };
</script>

<Router {routes} />

<style>
</style>
