<script lang="ts">
    import Button from "./Button.svelte";

    interface Props {
        buttonId?: string,
        buttonTabIndex?: number,
        buttonClasses?: string,
        buttonChildren?: any,
        buttonChildrenClasses?: string,
        buttonIconLeft?: string,
        buttonIconLeftSize?: string,
        buttonIconRight?: string,
        buttonIconRightSize?: string,
        dropdownId?: string,
        dropdownClasses?: string,
        dropdownChildren?: any,
        opened?: boolean,
    }

    let {
        buttonId,
        buttonTabIndex,
        buttonClasses,
        buttonChildren,
        buttonChildrenClasses,
        buttonIconLeft,
        buttonIconLeftSize,
        buttonIconRight,
        buttonIconRightSize,
        dropdownId,
        dropdownClasses,
        dropdownChildren,
        opened,
    }: Props = $props();

    function toggleClick() {
        if (opened)
            close();
        else
            open();
    }

    function close() {
        opened = false;
    }

    function open() {
        opened = true;
    }
</script>

<div class="dropdown-button-wr">
    <Button id={buttonId}
            tabIndex={buttonTabIndex}
            classes="dropdown-button {buttonClasses ? buttonClasses : ''}"
            children={buttonChildren}
            childrenClasses={buttonChildrenClasses}
            active={opened}
            iconLeft={buttonIconLeft}
            iconLeftSize={buttonIconLeftSize}
            iconRight={buttonIconRight}
            iconRightSize={buttonIconRightSize}
            onClick={toggleClick} />

    {#if dropdownChildren && opened}
        <div class="content-container dropdown-container">
            {@render dropdownChildren()}
        </div>
    {/if}
</div>

<style lang="scss">
    .dropdown-button-wr {
        position: relative;

        .dropdown-container {
            position: absolute;
            z-index: 2;
            padding: 0.5rem;
            display: flex;
            flex-direction: column;
            gap: 0.1rem;
            white-space: nowrap;
        }
    }
</style>
