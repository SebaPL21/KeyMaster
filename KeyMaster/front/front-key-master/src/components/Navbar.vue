<template>
    <v-app-bar elevation="6" class="navbar">
        <div class="nav">
           <router-link to="/"> <h1 class="logo">KeyMaster</h1></router-link> 
            <div class="nav-links">
                <router-link to="#"><h2>Lekcje</h2></router-link> 
                <router-link to="/test"><h2>Test</h2></router-link> 
                <h2>Memy</h2> 
            </div>
            <div class="nav-signs-buttons">
                <router-link to="/signup"><button class=""> Zaloguj się </button> </router-link>
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