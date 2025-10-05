import { Component } from '@angular/core';
import { Categories } from "../categories/categories";
import { Colors } from "../colors/colors";

@Component({
  selector: 'app-specifications',
  imports: [Categories, Colors],
  templateUrl: './specifications.html',
  styleUrl: './specifications.css'
})
export class Specifications {

}
