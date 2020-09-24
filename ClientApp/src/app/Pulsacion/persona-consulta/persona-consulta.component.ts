import { Component, OnInit } from '@angular/core';
import { Persona } from 'src/app/models/persona';
import { PersonaService } from 'src/app/services/persona.service';

@Component({
  selector: 'app-persona-consulta',
  templateUrl: './persona-consulta.component.html',
  styleUrls: ['./persona-consulta.component.css']
})
export class PersonaConsultaComponent implements OnInit {

  personas: Persona[];
  searchText: string;
  constructor(private personaService: PersonaService) { }

  ngOnInit() {
    this.get();
  }

  get(): void{
    this.personas = this.personaService.get();
  }

}
