import { Component, OnInit } from '@angular/core';
import { Persona } from 'src/app/models/persona';
import { PersonaService } from 'src/app/services/persona.service';

@Component({
  selector: 'app-persona-registro',
  templateUrl: './persona-registro.component.html',
  styleUrls: ['./persona-registro.component.css']
})
export class PersonaRegistroComponent implements OnInit {

  persona: Persona;

  constructor(private presonaService: PersonaService) { }

  ngOnInit() {
    this.persona =  new Persona;
  }

  calcularPulsation(): void
  {
    if (this.persona.sex == 'F' ) {
      this.persona.pulsation = (220 - this.persona.age) / 10;
    }else
    {
      this.persona.pulsation = (210 - this.persona.age) / 10;
    }
  
  }
  add(): void{
    this.calcularPulsation();
    alert("Se agregó una nueva persona" + JSON.stringify(this.persona));
    this.presonaService.post(this.persona);

  }

}
