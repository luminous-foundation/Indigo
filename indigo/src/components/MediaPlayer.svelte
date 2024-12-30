<script>
    import Icon from "@iconify/svelte";
    import { playing, player, location, currentSong, volume } from "../store";
    import { onMount } from "svelte";
    import { seekNext } from "$lib";
    import { SvelteURL } from "svelte/reactivity";

    onMount(() => {
        if($player == null) {
            let audio = new Audio();
            
            audio.addEventListener('ended', async () => {
                audio.pause();

                let song = seekNext();
                await loadPlayAudio(song);
            });

            audio.ontimeupdate = function() {
                $location = $player.currentTime
            }

            $player = audio;
        }
    })

    async function toggleAudio() {
        if($playing) {
            pauseAudio();
        } else {
            playAudio();
        }
    }

    async function pauseAudio() {
        $playing = false;

        if($player == null) {
            return;
        }

        $player.pause();
    }

    async function playAudio() {
        if($player == null) {
            return;
        }

        $playing = true;
        $player.play();
    }

    async function seekAudio() {
        $player.currentTime = Math.round($location);
    }

    function convertSecondsToMMSS(seconds) {
        const date = new Date(null);
        date.setSeconds(seconds);
        return date.toISOString().substr(14, 5); 
    }

    export async function loadPlayAudio(song) {
        $currentSong = song;

        if($player == null) {
            let audio = new Audio();
            
            audio.addEventListener('ended', () => {
                audio.pause();
                
                let song = seekNext();
                loadPlayAudio(song);
            });

            audio.ontimeupdate = function() {
                $location = $player.currentTime
            }

            $player = audio;
        }

        let buff = (await fetch("http://localhost:24896/songs", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(song)
        }));

        let audioBlob = await buff.blob();
        let audioUrl = SvelteURL.createObjectURL(audioBlob);

        $player.src = audioUrl;

        $playing = true;
        $player.play();
    }

    function setVol() {
        $player.volume = $volume / 100;
    }

</script>

<div class="media-player">
    {#if $player}
    <div class="duration-slider">
        <div class="centered-side">
            <input id="dur" class="duration-input" type="range" min="0" max={$player.duration} bind:value={$location} on:change={() => seekAudio()}/>
            <p class="timestamp">{convertSecondsToMMSS($location) +  ""}</p>
        </div>
    </div>
    {/if}

    <div class="playbutton" on:click={() => toggleAudio()}>
        {#if $playing}
        <Icon icon="line-md:pause" width="2em" height="2em" />
        {/if}
        
        {#if $playing == false}
        <Icon icon="line-md:play-filled" width="2em" height="2em" />
        {/if}
    </div>

    {#if $player}
    <div class="audio-slider">
        <div class="centered-side">
            <input id="dur" class="audio-input" type="range" min="0" max="100" bind:value={$volume} on:change={() => setVol()}/>
            <p class="timestamp">{$volume + "%"}</p>
        </div>
    </div>
    {/if}
</div>