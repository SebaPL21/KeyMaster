<template>
  <Navbar></Navbar>
  <v-container>
  <div class="content">
    <div class="userMainStats" v-for="item in userData" :key="item.allLessons">
      <v-avatar size="100"><v-img src="../assets/logo2.png"></v-img></v-avatar>
      <h3>Ilość odbytych lekcji {{item.allLessons }}</h3> 
      <h3>Średnia prędkość {{ item.avgSpeed }}</h3> 
      <h3>Średnia dokłacność {{ item.avgAccuracy}}</h3> 
    </div>
    
    <v-table>
    <thead>
      <tr>
        <th class="text-left">
          Tytul
        </th>
        <th class="text-left">
          Średnia prędkość WPM
        </th> 
        <th class="text-left">
          Błędy
        </th>
        <th class="text-left">
          Regularność
        </th> 
        <th class="text-left">
          Ocena
        </th>
        <th class="text-left">
          Kiedy
        </th>
      </tr>
    </thead>
    <tbody>
      <tr
        v-for="item in desserts"
        :key="item.name"
      >
        <td>{{ item.name }}</td>
        <td>{{ item.calories }}</td>
      </tr>
    </tbody>
  </v-table>
  </div>
</v-container>
<FooterVue></FooterVue>
  </template>
  
  <script lang="ts">
  import '@/styles/profileStyle.scss'
  import Navbar from "../components/Navbar.vue"
  import FooterVue from '../components/Footer.vue';
  export default {
    name: 'Profile',
    components: {
    Navbar,FooterVue,
  },
    props: {
      name: {
        type: String,
        required: true
      },
      bio: String
    },
    data () {
      return {
        userData:[
          {
          allLessons: 10,
          avgSpeed: 30,
          avgAccuracy:40,
        }
        ],
        desserts: [
          {
            name: 'Frozen Yogurt',
            calories: 159,
          },
          {
            name: 'Ice cream sandwich',
            calories: 237,
          },
          {
            name: 'Eclair',
            calories: 262,
          },
          {
            name: 'Cupcake',
            calories: 305,
          },
          {
            name: 'Gingerbread',
            calories: 356,
          },
          {
            name: 'Jelly bean',
            calories: 375,
          },
          {
            name: 'Lollipop',
            calories: 392,
          },
          {
            name: 'Honeycomb',
            calories: 408,
          },
          {
            name: 'Donut',
            calories: 452,
          },
          {
            name: 'KitKat',
            calories: 518,
          },
        ],
        }
      }
    
  }
  </script>
  
  <style>
  .profile {
    display: flex;
    flex-direction: column;
    align-items: center;
  }
  
  .profile img {
    width: 200px;
    border-radius: 50%;
  }
  </style>