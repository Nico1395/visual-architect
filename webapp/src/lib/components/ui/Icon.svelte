<script lang="ts">
    interface Props {
        icon: string,
        size?: string | null,
    }
    
    let { icon, size = "15px" }: Props = $props();
</script>

<i class={icon} style="font-size:{size}"></i>
