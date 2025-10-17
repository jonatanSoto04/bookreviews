import { Routes } from '@angular/router';
import { HomePage } from './pages/home/home-page';
import { BookPageComponent } from './pages/book/book-page';
import { LoginComponent } from './components/login/login';
import { RegisterComponent } from './components/register/register';

export const routes: Routes = [
  { path: '', component: HomePage },
  { path: 'book/:id', component: BookPageComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '**', redirectTo: '' }
];
