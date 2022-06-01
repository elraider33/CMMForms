import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
@Component({
  selector: 'app-delete-dialog',
  templateUrl: './delete-dialog.component.html',
  styleUrls: ['./delete-dialog.component.scss']
})
export class DeleteDialogComponent implements OnInit {
  type: string;
  id: string;
  confirmedId: string;
  constructor(
    public dialogRef: MatDialogRef<DeleteDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {type: string, id: string}
  ) { }

  ngOnInit() {
    this.type = this.data.type;
    this.id = this.data.id;
  }
  onConfirm(){
    if(this.confirmedId === this.id){
      this.dialogRef.close(true);
    }
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
}
