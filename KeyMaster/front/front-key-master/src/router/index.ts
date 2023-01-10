import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import MainPage from "../views/MainPage.vue";
import SignupPage from "../components/SignPage.vue";
import TestPage from "../views/Test.vue";
import Profile from "../views/Profil.vue"
const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "home",
    component: MainPage,
  },
  {
    path: "/signup",
    name: "SignupPage",
    component: SignupPage
  },
  {
    path: "/test",
    name: "Test",
    component: TestPage,
  },  
  {
    path: "/profile",
    name: "Profile",
    component: Profile,
  },
];


const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
