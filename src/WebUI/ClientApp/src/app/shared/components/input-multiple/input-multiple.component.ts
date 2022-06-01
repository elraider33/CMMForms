import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'input-multiple',
  templateUrl: './input-multiple.component.html',
  styleUrls: ['./input-multiple.component.scss']
})
export class InputMultipleComponent implements OnInit {
  value = new FormControl('');
  @Input() list: string[];
  @Output() onAddValue = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }
  addValue(){
    if(this.value.value){
      this.onAddValue.emit(this.value.value);
    }
  }
  removeValue(index: number, value:string){
    this.list.splice(index, 1);
  }
}
