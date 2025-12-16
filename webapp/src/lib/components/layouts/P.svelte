<script lang="ts">
    let { children } = $props();
</script>

<h2 class="p">
    {@render children()}
</h2>

<style lang="scss">
    .p {
        font-size: inherit;
        font-weight: inherit;
        margin: 1em 0;
    }
</style>
