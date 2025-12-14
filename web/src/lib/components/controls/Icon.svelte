<script lang="ts">
    interface Props {
        icon: string | undefined,
        size?: string,
    }

    let {
        icon,
        size,
    }: Props = $props();
</script>

{#if icon != undefined && icon != null}
    <i class="{icon}" style="font-size:{size};"></i>
{/if}
