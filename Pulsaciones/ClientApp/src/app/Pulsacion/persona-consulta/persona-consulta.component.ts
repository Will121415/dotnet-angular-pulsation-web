import { Component, OnInit } from '@angular/core';
import { Person } from 'src/app/models/person';
import { PersonaService } from 'src/app/services/persona.service';

@Component({
  selector: 'app-persona-consulta',
  templateUrl: './persona-consulta.component.html',
  styleUrls: ['./persona-consulta.component.css']
})
export class PersonaConsultaComponent implements OnInit {

  persons: Person[];
  searchText: string;
  constructor(private personaService: PersonaService) { }

  ngOnInit() {
    this.personaService.get().subscribe(result => { this.persons = result;});
  }
}
