# ObjectiveGUI

## Overview
ObjectiveGUI is a modular UI framework for .NET Framework 4.8 with Unity Engine integration. It provides a structured approach to building UIs with clean separation of concerns and abstraction layers for potential multi-platform support.

## Key Features
- **Modular Architecture**: Independent, interchangeable modules
- **Event System**: Pipelines, prefabs, and Unity integration
- **Visual Components**: Customizable UI elements library
- **Layout System**: Flexible positioning and container management
- **Data Binding**: Observable properties with animation support
- **Transformation**: Position, scale, and rotation utilities

## Project Structure

### Core Modules

**Event System**
- **OG.Event**: Event handlers, callbacks, and providers
- **OG.Event.Pipe**: Pipeline for event processing
- **OG.Event.Prefab**: Pre-built event types

**Element System**
- **OG.Element**: Base UI components
- **OG.Element.Container**: Child management components
- **OG.Element.Interactive**: User input handling elements
- **OG.Element.Visual**: Rendering-focused elements

**Graphics System**
- **OG.Graphics**: Line, quad, text, and texture rendering

**Layout System**
- **OG.Layout**: UI element positioning and arrangement

**Transformer System**
- **OG.Transformer**: Position, scale, and rotation utilities

**Factory & Builder**
- **OG.Factory**: Element creation factories
- **OG.Builder**: Complex UI construction utilities

**Utilities**
- **OG.TextController**: Text management
- **OG.DataKit**: Animation, binding, and transformations
- **OG.DataTypes**: UI-specific type definitions

Each module follows a consistent pattern with abstraction interfaces (.Abstraction), core implementations, and specialized extensions.

## Key Systems

### Event System
Transforms Unity events into framework-specific events through a pipeline architecture:
- **Handlers & Callbacks**: Process typed events with specific responses
- **Event Pipes**: Filter and transform events between Unity and ObjectiveGUI
- **Prefabs**: Pre-configured event types for common scenarios

### Graphics System
Abstracted rendering capabilities with specialized implementations:
- **Line, Quad, Text, Texture**: Specific renderers for different UI needs
- **Unity Integration**: Uses Unity's rendering while maintaining abstraction

### Element System
The building blocks of interfaces with generic type support:
- **Interactive Elements**: User input handling with binding capabilities
- **Container Elements**: Manage parent-child relationships
- **Visual Elements**: Rendering-focused with no interaction

### Transformer System
Handles UI element positioning and appearance:
- **Rect Calculation**: Dynamic rectangle sizing and positioning
- **Transform Operations**: Position, scale, and rotation utilities

### Layout System
Manages element arrangement with events for layout changes and positioning calculations based on constraints.

## Design Patterns

- **Factory**: Element creation without concrete class coupling
- **Builder**: Step-by-step UI component construction
- **Observer**: Reactive programming via observable properties
- **Strategy**: Runtime algorithm selection for rendering and events
- **Composite**: Element hierarchy with container management
- **Dependency Injection**: Interface-based design for loose coupling
- **Command**: Action encapsulation in interactive elements
- **Pipe and Filter**: Event processing pipeline

## Architecture

ObjectiveGUI uses a layered architecture with SOLID principles:

1. **Abstraction Layer**: Interfaces with `IOg` prefix and generic typing
2. **Core Implementation Layer**: Base functionality implementations
3. **Extension Layer**: Specialized elements and utility extensions
4. **Context Layer**: Builder contexts and service providers
5. **Integration Layer**: Unity Engine connectivity with abstraction

**SOLID Implementation:**
- **Single Responsibility**: Focused classes with clear purposes
- **Open/Closed**: Extensibility through abstractions
- **Liskov Substitution**: Interface-based polymorphism
- **Interface Segregation**: Specific, focused interfaces
- **Dependency Inversion**: Abstraction-dependent components

## Advantages

- **Clean Architecture**: Strict separation of concerns with modular design
- **Type Safety**: Extensive generics for compile-time error detection
- **Data Binding**: Observable properties with two-way binding support
- **Event System**: Custom pipeline with type-safe event handling
- **Fluent API**: Builder pattern with context-aware construction
- **Testability**: Interface-based design for effective unit testing
- **Performance**: Optimized rendering and selective event processing

## Getting Started

## Requirements
- .NET Framework 4.8
- Visual Studio 2019 or higher
- Unity Engine

## License
See the [LICENSE](LICENSE) file for details.
