import * as React from 'react';
import { Label } from '@fluentui/react';

export interface ICircleProgressProps {
  name?: string;
}

export class CircleProgress extends React.Component<ICircleProgressProps> {
  public render(): React.ReactNode {
    return (
      <Label>
        {this.props.name}
      </Label>
    )
  }
}
