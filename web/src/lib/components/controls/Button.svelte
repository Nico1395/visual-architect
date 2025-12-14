<script lang="ts">
    import Icon from './Icon.svelte';

    interface Props {
        id?: string,
        tabIndex?: number,
        classes?: string,
        childrenClasses?: string,
        children?: any,
        onClick?: any,
        active?: boolean,
        iconLeft?: string,
        iconLeftSize?: string,
        iconRight?: string,
        iconRightSize?: string,
    }

    let {
        id,
        tabIndex,
        classes,
        children,
        childrenClasses,
        onClick,
        active = false,
        iconLeft,
        iconLeftSize,
        iconRight,
        iconRightSize,
    }: Props = $props();
</script>

<button id="{id}" tabindex="{tabIndex}" class="clickable flat button {active ? "active" : ''} {classes}" onclick={onClick}>
    <Icon icon={iconLeft} size={iconLeftSize} />

    <div class="children {childrenClasses}">
        {@render children()}
    </div>

    <Icon icon={iconRight} size={iconRightSize} />
</button>

<style type="scss">
    .button {
        padding: 0.7rem;
        border-radius: 20px;
        height: 35px;
        display: flex;
        flex-direction: row;
        flex-wrap: nowrap;
        align-items: center;
        gap: 0.25rem;
        box-sizing: border-box;

        &.active {

        }
    }
</style>
