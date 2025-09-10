#!/bin/bash

# Clean Architecture Template Generator
# Converts Studyville.TutorTrack to BLH.ApproveIQ

set -e

# Configuration
SOURCE_DIR="/Users/bomanry/Development/Sparkhound/TutorTrack"
TARGET_DIR="/Users/bomanry/Development/Sparkhound/ApproveIQ"
OLD_NAMESPACE="Studyville.TutorTrack"
NEW_NAMESPACE="BLH.ApproveIQ"
OLD_PROJECT_NAME="TutorTrack"
NEW_PROJECT_NAME="ApproveIQ"

echo "🚀 Creating Clean Architecture template for $NEW_NAMESPACE"
echo "Source: $SOURCE_DIR"
echo "Target: $TARGET_DIR"

# Create target directory
if [ -d "$TARGET_DIR" ]; then
    echo "❌ Target directory already exists. Please remove it first or choose a different name."
    exit 1
fi

mkdir -p "$TARGET_DIR"

# Copy the entire project structure, excluding build artifacts and user-specific files
echo "📁 Copying project structure..."
rsync -av --progress \
    --exclude='bin/' \
    --exclude='obj/' \
    --exclude='*.user' \
    --exclude='.vs/' \
    --exclude='packages/' \
    --exclude='*.suo' \
    --exclude='*.cache' \
    --exclude='node_modules/' \
    --exclude='dist/' \
    --exclude='ClientApp/dist/' \
    --exclude='ClientApp/node_modules/' \
    "$SOURCE_DIR/" "$TARGET_DIR/"

echo "🔄 Renaming files and folders..."

# Function to rename files and directories
rename_items() {
    find "$TARGET_DIR" -depth -name "*$OLD_PROJECT_NAME*" | while read OLD_PATH; do
        NEW_PATH=$(echo "$OLD_PATH" | sed "s/$OLD_PROJECT_NAME/$NEW_PROJECT_NAME/g")
        if [ "$OLD_PATH" != "$NEW_PATH" ]; then
            mv "$OLD_PATH" "$NEW_PATH"
            echo "Renamed: $(basename "$OLD_PATH") -> $(basename "$NEW_PATH")"
        fi
    done
}

rename_items

echo "🔍 Replacing namespace references in files..."

# Function to replace content in files
replace_in_files() {
    local file_pattern="$1"
    local old_text="$2"
    local new_text="$3"
    
    find "$TARGET_DIR" -name "$file_pattern" -type f -exec grep -l "$old_text" {} \; | while read file; do
        sed -i.bak "s|$old_text|$new_text|g" "$file"
        rm "$file.bak"
        echo "Updated: $file"
    done
}

# Replace namespaces in C# files
replace_in_files "*.cs" "$OLD_NAMESPACE" "$NEW_NAMESPACE"

# Replace in project files
replace_in_files "*.csproj" "$OLD_NAMESPACE" "$NEW_NAMESPACE"
replace_in_files "*.csproj" "$OLD_PROJECT_NAME" "$NEW_PROJECT_NAME"

# Replace in solution file
replace_in_files "*.sln" "$OLD_NAMESPACE" "$NEW_NAMESPACE"
replace_in_files "*.sln" "$OLD_PROJECT_NAME" "$NEW_PROJECT_NAME"

# Replace in JSON files (appsettings, etc.)
replace_in_files "*.json" "$OLD_NAMESPACE" "$NEW_NAMESPACE"

# Replace in YAML pipeline files
replace_in_files "*.yml" "studyville" "approveiq"
replace_in_files "*.yml" "Studyville" "BLH"
replace_in_files "*.yml" "$OLD_PROJECT_NAME" "$NEW_PROJECT_NAME"

echo "🧹 Cleaning up domain-specific content..."

# Clean up domain-specific entities (keep base classes and interfaces)
ENTITIES_DIR="$TARGET_DIR/BLH.ApproveIQ.Domain/Entities"
if [ -d "$ENTITIES_DIR" ]; then
    # Keep common base classes, remove specific entities
    find "$ENTITIES_DIR" -name "*.cs" -not -name "*Base*" -not -name "*Abstract*" -not -name "IAuditableEntity*" -exec rm {} \;
fi

# Clean up specific DTOs and requests (keep common ones)
DTOS_DIR="$TARGET_DIR/BLH.ApproveIQ.Application/DTOs"
if [ -d "$DTOS_DIR" ]; then
    find "$DTOS_DIR" -name "*.cs" -not -name "*Base*" -not -name "*Common*" -exec rm {} \;
fi

REQUESTS_DIR="$TARGET_DIR/BLH.ApproveIQ.Application/Requests"
if [ -d "$REQUESTS_DIR" ]; then
    find "$REQUESTS_DIR" -name "*.cs" -not -name "*Base*" -not -name "*Command*" -not -name "*Query*" | head -10 | xargs rm -f
fi

# Clean up specific controllers (keep base controller)
CONTROLLERS_DIR="$TARGET_DIR/BLH.ApproveIQ.Presentation/Controllers"
if [ -d "$CONTROLLERS_DIR" ]; then
    find "$CONTROLLERS_DIR" -name "*.cs" -not -name "*Base*" -not -name "*Abstract*" -exec rm {} \;
fi

echo "📝 Creating sample template files..."

# Create a sample entity
cat > "$TARGET_DIR/BLH.ApproveIQ.Domain/Entities/SampleEntity.cs" << 'EOF'
using BLH.ApproveIQ.Domain.Primitives;

namespace BLH.ApproveIQ.Domain.Entities;

public sealed class SampleEntity : Entity
{
    public SampleEntity(Guid id, string name) : base(id)
    {
        Name = name;
        CreatedOn = DateTime.UtcNow;
    }

    private SampleEntity() : base(Guid.Empty) { }

    public string Name { get; private set; } = string.Empty;
    public DateTime CreatedOn { get; private set; }
    public DateTime? UpdatedOn { get; private set; }

    public void UpdateName(string name)
    {
        Name = name;
        UpdatedOn = DateTime.UtcNow;
    }
}
EOF

# Create a sample controller
cat > "$TARGET_DIR/BLH.ApproveIQ.Presentation/Controllers/SampleController.cs" << 'EOF'
using BLH.ApproveIQ.Presentation.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BLH.ApproveIQ.Presentation.Controllers;

public class SampleController : ApiController
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { Message = "BLH.ApproveIQ API is running!" });
    }
}
EOF

# Create basic README
cat > "$TARGET_DIR/README.md" << EOF
# BLH.ApproveIQ

A Clean Architecture .NET 8 application following Domain-Driven Design principles.

## Architecture

This solution follows Clean Architecture with the following layers:

- **Domain**: Core business logic and entities
- **Application**: Use cases and business rules  
- **Infrastructure**: External services integration
- **Persistence**: Data access layer
- **Presentation**: Controllers and API contracts
- **API**: Web API host
- **Portal**: Web frontend application

## Getting Started

1. Update connection strings in appsettings.json
2. Run database migrations
3. Build and run the solution

## Projects

- \`BLH.ApproveIQ.Domain\` - Domain entities and business logic
- \`BLH.ApproveIQ.Application\` - Application services and DTOs
- \`BLH.ApproveIQ.Infrastructure\` - External service integrations
- \`BLH.ApproveIQ.Persistence\` - Entity Framework and repositories
- \`BLH.ApproveIQ.Presentation\` - API controllers
- \`BLH.ApproveIQ.API\` - Web API startup and configuration
- \`BLH.ApproveIQ.Portal\` - Frontend web application

Generated from TutorTrack template on $(date)
EOF

echo "✅ Template creation complete!"
echo ""
echo "📍 Your new project is located at: $TARGET_DIR"
echo ""
echo "🔧 Next steps:"
echo "1. Open the solution in your IDE"
echo "2. Update connection strings in appsettings.json files"
echo "3. Review and customize the remaining configuration"
echo "4. Add your domain entities and business logic"
echo "5. Run dotnet restore to restore packages"
echo ""
echo "🏗️  Architecture preserved:"
echo "   ✓ Clean Architecture structure"
echo "   ✓ Dependency injection setup"
echo "   ✓ Entity Framework configuration"
echo "   ✓ AutoMapper profiles"
echo "   ✓ MediatR patterns"
echo "   ✓ Repository pattern"
echo "   ✓ Background job setup"
echo "   ✓ Authentication/authorization framework"
echo ""
