import { Routes } from '@angular/router';
import { CartHomeComponent } from './Pages/cart-home/cart-home.component';
import { HomeComponent } from './Pages/LandingPage/home/home.component';
import { PackageEditComponent } from './Pages/Packagess/PackageEdit/package-edit/package-edit.component';
import { PackageAddComponent } from './Pages/Packagess/PackageAdd/package-add/package-add.component';
import { PackageDeleteComponent } from './Pages/Packagess/PackageDelete/package-delete/package-delete.component';
import { PackageListComponent } from './Pages/Packagess/PackageList/package-list/package-list.component';
import { AboutUsComponent } from './Pages/LandingPage/about-us/about-us.component';
import { LoginComponent } from './Pages/login/login.component';
import { RegisterComponent } from './Pages/register/register.component';

import { ServicesListComponent } from './Pages/Services/services-list/services-list.component';

import { logginGuardGuard } from './Guard/loggin-guard.guard';


export const routes: Routes = [
    { path: 'Cart/:userId', component: CartHomeComponent, canActivate:[logginGuardGuard] },
    { path: 'Package', component:PackageListComponent,title:"Packages" },
    {path:'Package/delete/:id',component:PackageDeleteComponent,title:'Delete'},
    {path:'Package/add',component:PackageAddComponent,title:'Add'},
    {path:'Package/edit/:id',component:PackageEditComponent,title:'Edit'},
    {path: 'Home', component: HomeComponent, title:'Home', children:[
        {path:'About',component:AboutUsComponent,title:'About'},
    ]},
    {path: 'login', component:LoginComponent, title:'Login'},
    {path: 'Register', component:RegisterComponent, title:'Register'},
    {path: 'Service/:category',component:ServicesListComponent,title:"Services"},
    {path: '', redirectTo: 'Home', pathMatch: "full"}
];
