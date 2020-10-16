import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PersonaConsultaComponent } from './Pulsacion/persona-consulta/persona-consulta.component';
import { PersonaRegistroComponent } from './Pulsacion/persona-registro/persona-registro.component';


const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  {path: 'personaConsulta', component: PersonaConsultaComponent},
  {path: 'personaRegistro', component: PersonaRegistroComponent},
];


@NgModule({
  declarations: [],
  
  imports: [ 
    CommonModule ,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]

})
export class AppRoutingModule { }
