<template>
    <v-app-bar elevation="6" class="navbar">
        <div class="nav">
        <h1 class="logo">KeyMaster</h1>
        <div class="nav-links">
            <h2>lekcje</h2> 
            <h2>testy</h2> 
            <h2>Memy</h2> 
            
        </div>
        <div class="nav-signs-buttons">
            <router-link to="/signup"><button> Zaloguj się> </button> </router-link>
            <a>Rejestracja</a> 
        </div>
    </div>
</v-app-bar>

</template>

<script lang="ts">
    import '@/styles/style.scss'
    export default {
        name: "NavbarComponent",
     
    }
</script>