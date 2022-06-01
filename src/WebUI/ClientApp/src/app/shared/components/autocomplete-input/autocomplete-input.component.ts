import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith,} from 'rxjs/operators';
import { AutoCompleteModel } from 'src/app/core/interfaces/autocomplete.interface';

@Component({
  selector: 'autocomplete-input',
  templateUrl: './autocomplete-input.component.html',
  styleUrls: ['./autocomplete-input.component.scss']
})
export class AutocompleteInputComponent implements OnInit {

  myControl = new FormControl();
  @Input() options: string[];
  @Input() list: string[];
  @Input() type: string;
  @Output() onSelectedElement = new EventEmitter<string[]>();
  @Output() onRemoveElement = new EventEmitter<string[]>();
  filteredOptions: Observable<string[]>;

  ngOnInit() {
    this.filteredOptions = this.myControl.valueChanges.pipe(
      map(text => this._filter(text)),
    );
  }

  displayFn(data: string): string {
    return data;
  }

  private _filter(text: string): string[] {
    const filterValue = text.toLowerCase();
    return this.options.filter(option => option.toLowerCase().includes(filterValue));
  }
  onSelectHandler(event){
    this.list.push(event.option.value);
    this.onSelectedElement.emit(this.list);
  }
  removeValueHandler(index: number, value:string){
    this.list.splice(index, 1);
    this.onRemoveElement.emit(this.list);
  }
}
